using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MathQuiz
{
    /**

The MainForm class is the main form of the math game application. It contains fields for storing user information such as the user's name, number of questions, and number of correct answers.
It also contains fields for storing information about the current math problem being displayed, such as the operator, values, and answer.
The class utilizes a random number generator to create new math problems and has a field for storing the time limit for each problem.
Fields:
myName: stores the user's name
nQuestions: stores the total number of questions to be asked
sQuestions: stores the number of questions asked so far
nResponse: stores the user's numeric response to the current math problem
sResponse: stores the user's string response to the current math problem
nMaxRange: stores the maximum value that can be used for generating random values in math problems
nCorrect: stores the number of correct answers
bValid: stores whether the user's response to the current math problem is valid or not
rand: a Random object used to generate random numbers for math problems
nOp: stores the current math operator being used
val1: stores the first value of the current math problem
val2: stores the second value of the current math problem
nAnswer: stores the correct answer to the current math problem
nCntr: stores the total number of problems attempted so far
nTimeLimit: stores the time limit for each math problem
nTimeRemaining: stores the time remaining for the current math problem
*/
    public partial class MainForm : Form
    {
        private string myName = "";
        private int nQuestions = 0;
        private int sQuestions = 0;
        private int nResponse = 0;
        private string sResponse = "";
        private int nMaxRange = 0;
        private int nCorrect = 0;
        private int bValid = 0;
        
        private Random rand = new Random();

        private int nOp = 0;
        private int val1 = 0;
        private int val2 = 0;
        private int nAnswer = 0;
        private int nCntr = 0;

        private int nTimeLimit = 10;
        private int nTimeRemaining = 0;

        public MainForm()
        {
            InitializeComponent();
        }

       private void MainForm_Load(object sender, EventArgs e)
{
    // Displays a welcome message box when the form is loaded
    MessageBox.Show("Welcome to Math Quiz!", "Math Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
}

private void btnStart_Click(object sender, EventArgs e)
{
    // Prompt the user for their name and store it in the private variable 'myName'
    while (true)
    {
        myName = Microsoft.VisualBasic.Interaction.InputBox("What is your name?", "Math Quiz");
        if (myName.Length > 0)
        {
            break;
        }
    }

    // Start a new game
    StartNewGame();
}

        private void StartNewGame()
        {
            StartNewGame();
        }

        private void StartNewGame(int sResponse)
        {
            // reset values for a new game
            nCorrect = 0;
            nCntr = 0;

            // ask for number of questions
            while (true)
            {
                string sQuestions = Microsoft.VisualBasic.Interaction.InputBox("How many questions?", "Math Quiz");
                if (int.TryParse(sQuestions, out nQuestions) && nQuestions > 0)
                {
                    break;
                }
                MessageBox.Show("Please enter a positive integer.", "Math Quiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ask for difficulty level
            string[] difficulties = { "Easy", "Medium", "Hard" };
            string sDifficulty = Microsoft.VisualBasic.Interaction.InputBox("Select difficulty level:", "Math Quiz", "Easy", -1, -1);


            // set nMaxRange based on difficulty level
            switch (sDifficulty.ToLower())
            {
                case "easy":
                    nMaxRange = 10;
                    if (myName.ToLower() == "david")
                    {
                        goto case "hard";
                    }
                    break;

                case "medium":
                    nMaxRange = 20;
                    break;

                case "hard":
                    nMaxRange = 30;
                    break;

                default:
                    nMaxRange = 10;
                    break;
            }

            // ask for time limit
            while (true)
            {
                string sTimeLimit = Microsoft.VisualBasic.Interaction.InputBox("How many seconds per question?", "Math Quiz", "10");
                if (int.TryParse(sTimeLimit, out nTimeLimit) && nTimeLimit > 0)
                {
                    break;
                }
                MessageBox.Show("Please enter a positive integer.", "Math Quiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // initialize progress bar and timer
            nTimeRemaining = nTimeLimit;
            progressBar.Minimum = 0;
            progressBar.Maximum = nTimeLimit;
            progressBar.Value = nTimeLimit;
            timer.Interval = 1000;
            timer.Start();

            // ask each question
            // ask each question
            for (nCntr = 0; nCntr < nQuestions; ++nCntr)
            {
                // generate a random number between 0 inclusive and 3 exclusive to get the operation
                nOp = rand.Next(0, 3);
                val1 = rand.Next(0, nMaxRange) + nMaxRange;
                val2 = rand.Next(0, nMaxRange);

                // if either argument is 0, pick new numbers
                if (val1 == 0 || val2 == 0)
                {
                    // decrement counter to try this one again (because it will be incremented at the top of the loop)
                    --nCntr;
                    continue;
                }

                // if nOp == 0, then addition
                // if nOp == 1, then subtraction
                // else multiplication
                string sQuestions = "";

                if (nOp == 0)
                {
                    nAnswer = val1 + val2;
                    sQuestions = $"Question #{nCntr + 1}: {val1} + {val2} => ";
                }
                else if (nOp == 1)
                {
                    nAnswer = val1 - val2;
                    sQuestions = $"Question #{nCntr + 1}: {val1} - {val2} => ";
                }
                else
                {
                    nAnswer = val1 * val2;
                    sQuestions = $"Question #{nCntr + 1}: {val1} * {val2} => ";
                }

                // display the question and prompt for the answer

                int nResponse = 0;
                bool bValid = false;

                do
                {
                    {
                        Console.Write(sQuestions);
                        sResponse = Console.ReadLine();

                        if (int.TryParse(sResponse, out nResponse))
                        {
                            bValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter an integer.");
                            bValid = false;
                        }
                    }
                } while (!bValid);


                // prompt for a time limit
                Console.WriteLine("Please enter the time limit for this question (in seconds):");
                int timeLimit = int.Parse(Console.ReadLine());

                // create a timer with the time limit
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = timeLimit * 1000;
                timer.Enabled = true;
                timer.Start();

                // create a progress bar with the time limit
                // ProgressBar progressBar = new ProgressBar();
                progressBar.Maximum = timeLimit;
                progressBar.Step = 1;
                progressBar.BackColor = System.Drawing.Color.White;
                progressBar.ForeColor = System.Drawing.Color.Blue;
                progressBar.Width = 200;
                progressBar.Height = 25;
                progressBar.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - progressBar.Width) / 2;
                progressBar.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - progressBar.Height) / 2;

                // create a form to host the progress bar
                System.Windows.Forms.Form form = new System.Windows.Forms.Form();
                form.Text = "Time Left";
                form.ClientSize = new System.Drawing.Size(250, 50);
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                form.Controls.Add(progressBar);
                form.Show();

                // add an event handler to update the progress bar
                timer.Tick += (sender, e) =>
                {
                    progressBar.PerformStep();
                    if (progressBar.Value == progressBar.Maximum)
                    {
                        timer.Stop();
                        form.Close();

                        // display the result if the time limit has expired
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Time's up! You ran out of time for this question.");
                        Console.ResetColor();

                        // display a stern graphic
                        Console.WriteLine(" _____ _ _ _____ _ _ ");
                        Console.WriteLine("| || |||__ ___ ___ | _ |___ ||_ | | ");
                        Console.WriteLine("|__ | | | | | | -|| | | | | || |");
                        Console.WriteLine("|_____|| || ||||| || ||__|| || || | ||_|");
                        Console.WriteLine("");
                        // display the correct answer
                        int correctAnswer = 0;
                        Console.WriteLine($"The correct answer is: {correctAnswer}");
                    }
                };

                int numQuestions = 0;
                int numCorrectAnswers = 0;

                // calculate the percentage of correct answers
                double percentageCorrect = (double)numCorrectAnswers / numQuestions * 100;

                // display the final score
                Console.WriteLine($"You got {numCorrectAnswers} out of {numQuestions} correct ({percentageCorrect}%).");

                // ask the user if they want to play again
                Console.Write("Do you want to play again? (y/n): ");
                string playAgainResponse = Console.ReadLine().ToLower();

                // if the user wants to play again, clear the console and start over
                if (playAgainResponse == "y")
                {
                    Console.Clear();
                    StartNewGame();
                }
                else
                {
                    // display a farewell message and exit the program
                    Console.WriteLine("Thanks for playing! Goodbye.");
                    Environment.Exit(0);
                }
            }
        }
    }
}
