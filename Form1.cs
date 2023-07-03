using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;


namespace WinformsLineGenerator;

public partial class Form1 : Form
{
    private List<Thread> threads = new List<Thread>();
    private Random random = new Random();
    private bool stopThreads = false;
    private bool started = false;

    private OleDbConnection mdbConnection = new OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Kestutis\\Desktop\\LineGenerator\\WinformsLineGenerator\\Database1.mdb");
    private SqlConnection sqlsConnection = new SqlConnection(
        "Server=localhost; Initial Catalog=LineGenerator; Integrated Security=true");

    public Form1()
    {
        InitializeComponent();
        //create table in SQL Server database if table is not present
        sqlsConnection.Open();
        DataTable dTable = sqlsConnection.GetSchema("TABLES", new string[] { null, null, "Lines" });
        if(dTable.Rows.Count == 0)
        {
            string query =
            @"CREATE TABLE dbo.Lines
                (
                    Id int IDENTITY(1,1) NOT NULL,
                    ThreadID int NOT NULL,
                    TimeCreated datetime NOT NULL,
                    Data varchar(16) NOT NULL,
                    CONSTRAINT pk_id PRIMARY KEY (Id)
                );";
            SqlCommand command = new SqlCommand(query, sqlsConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating table in SQL Server Database. \nDetails: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        sqlsConnection.Close();

        //create table in Access database if table is not present
        mdbConnection.Open();
        dTable = mdbConnection.GetSchema("TABLES", new string[] { null, null, "Lines" });
        if (dTable.Rows.Count == 0)
        {
            string query =
            @"CREATE TABLE Lines
                (
                    Id AUTOINCREMENT PRIMARY KEY,
                    ThreadID NUMBER NOT NULL,
                    TimeCreated DATETIME NOT NULL,
                    Data TEXT(16) NOT NULL
                );";
            OleDbCommand command = new OleDbCommand(query, mdbConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating table in Access Database. \nDetails: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        mdbConnection.Close();
    }
    /// <summary>
    /// Control user's input in textbox
    /// Allow user to specify number of threads to be started from 2 to 15
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        var count = 0;
        //Allow user to input numbers from 1 to 15
        if (!((char.IsDigit(e.KeyChar) && int.TryParse(textBox1.Text + e.KeyChar, out count) && count > 0 && count <= 15) ||
            (e.KeyChar == '\b')))
        {
            e.Handled = true;
        }
        if (count >= 2 && count <= 15)
        {
            startButton.Enabled = true;
        }
        else
        {
            startButton.Enabled = false;
        }
    }

    private void startButton_Click(object sender, EventArgs e)
    {
        startButton.Enabled = false;
        if (!started)
        {
            started = true;
            startButton.Text = "Stop";
            mdbCheckBox.Enabled = false;
            sqlsCheckBox.Enabled = false;
            Start();
        }
        else
        {
            started = false;
            startButton.Text = "Start";
            mdbCheckBox.Enabled = true;
            sqlsCheckBox.Enabled = true;
            Stop();
        }
    }
    /// <summary>
    /// Clear data from listView and databases
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void clearButton_Click(object sender, EventArgs e)
    {
        listView1.Items.Clear();
        string query = "DELETE FROM Lines";
        OleDbCommand mdbCommand = new OleDbCommand(query, mdbConnection);
        SqlCommand sqlsCommand = new SqlCommand(query, sqlsConnection);
        try
        {
            mdbCommand.ExecuteNonQuery();
            sqlsCommand.ExecuteNonQuery();
        }
        catch (Exception clearException)
        {
            MessageBox.Show("Error clearing data from database. \nDetails: " + clearException.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    /// <summary>
    /// Start specified amount of threads
    /// </summary>
    private void Start()
    {
        if (mdbCheckBox.Checked)
        {
            mdbConnection.Open();
        }
        if (sqlsCheckBox.Checked)
        {
            sqlsConnection.Open();
        }

        stopThreads = false;
        int numThreads;
        if(int.TryParse(textBox1.Text, out numThreads))
        {
            for (int i = 1; i <= numThreads; i++)
            {
                int threadID = i;
                Thread thread = new Thread(() => GenerateStrings(threadID));
                threads.Add(thread);
                thread.Start();
            }
        }
        else
        {
            Stop();
        }
        startButton.Enabled = true;
    }
    /// <summary>
    /// Stop all threads and wait for them to finish
    /// </summary>
    private void Stop()
    {
        stopThreads = true;
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        threads.Clear();
        mdbConnection.Close();
        sqlsConnection.Close();
        startButton.Enabled = true;
    }
    /// <summary>
    /// Task for threads to generate strings
    /// Generated data is added to listView element
    /// </summary>
    /// <param name="threadId">Thread number / id</param>
    private void GenerateStrings(int threadId)
    {
        while (!stopThreads)
        {
            int length = random.Next(5, 11);
            string generatedString = GenerateRandomString(length);

            Invoke(new Action(() =>
            {
                listView1.Items.Insert(0, new ListViewItem(new[] { threadId.ToString(), generatedString }));
                if (listView1.Items.Count > 20)
                {
                    listView1.Items.RemoveAt(20);
                }
                WriteDataToDatabase(threadId, DateTime.Now, generatedString);
            }));
            Thread.Sleep(random.Next(500, 2001));
        }
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    /// <summary>
    /// Writes data into database if checkbox is checked
    /// </summary>
    /// <param name="threadID"></param>
    /// <param name="time"></param>
    /// <param name="generatedLine"></param>
    private void WriteDataToDatabase(int threadID, DateTime time, string generatedLine)
    {
        string query = "INSERT INTO Lines (ThreadID, TimeCreated, Data)" + " VALUES (@ThreadID,@Time,@Data)";

        if (mdbCheckBox.Checked)
        {
            try
            {
                using (OleDbCommand command = new OleDbCommand(query, mdbConnection))
                {
                    command.Parameters.AddWithValue("@ThreadID", threadID);
                    command.Parameters.AddWithValue("@Time", time.ToString());
                    command.Parameters.AddWithValue("@Data", generatedLine);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error adding data to Access database. \nDetails: " + exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        if(sqlsCheckBox.Checked)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, sqlsConnection))
                {
                    command.Parameters.AddWithValue("@ThreadID", threadID);
                    command.Parameters.AddWithValue("@Time", time.ToString());
                    command.Parameters.AddWithValue("@Data", generatedLine);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error adding data to SQL Server database. \nDetails: " + exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    /// <summary>
    /// Handle form close event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Stop();
    }
}
