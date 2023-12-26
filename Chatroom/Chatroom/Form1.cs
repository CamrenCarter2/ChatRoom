using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatroom
{
    public partial class Form1 : Form
    {

        // Variables
        bool goLeft;
        bool goRight;
        bool goUp;
        bool goDown;
        bool buttonClicked = false;
        readonly int speed = 10;
        int slideDist = 60;
        int connections = 0;
        PictureBox client;
        PictureBox host;
        //private PictureBox[] users;

        public Form1()
        {
            InitializeComponent();

            //users = new PictureBox[5];



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SETUP
            panel.Width = (Size.Width - 70);
            panel.Height = (Size.Height - 100);

            textPanel.Left = 0;
            textPanel.Top = (Size.Height - 50);



            // Starts the Timer which handles movement and updates
            timer.Start();

            // Disable text-related components initially
            textChange.Enabled = false;
            button1.Enabled = false;

            // Let the user choose between client and server modes
            DialogResult result = MessageBox.Show("Choose the application mode: \nClient: Click OK\nServer: Click Cancel", "Application Mode", MessageBoxButtons.OKCancel);

            // Start as a client if OK is clicked or as a server if Cancel is clicked
            ConnectionHandler connectionHandler = new ConnectionHandler();

            // Event handler for received messages
            connectionHandler.MessageReceived += (sender1, message) =>
            {
                Console.WriteLine($"Received message: {message}");
            };
            connectionHandler.HostPictureBoxReceived += (sender2, pictureBox) =>
            {
                // Use pictureBox to update the UI or handle the received PictureBox data
                // For example, add it to the panel
                panel.Controls.Add(pictureBox);
            };

            if (result == DialogResult.OK)
            {

                // Start as a client
                connectionHandler.StartClient("127.0.0.1", 8888);
                // Connection established, send a message from client to server
                connectionHandler.SendMessage("Hello from client");
                connections++;
                connectionHandler.SendHostPictureBox(host);
                client = CreateUser("Client " + connections, Color.Brown);
                panel.Controls.Add(client);

            }
            else if (result == DialogResult.Cancel)
            {
                
                // Start as a server
                connectionHandler.StartServer(8888);
                host = CreateUser("Host", Color.Blue);
                panel.Controls.Add(host);
                
            }
        }


        // When Key is Down the User will move
        private void keyisdown(object sender, KeyEventArgs e)
        {
            // User Movement
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            // When E is pressed  move the panel to a visible area then Enable the textbox and button
            if (e.KeyCode == Keys.E)
            {
                for (int i = 0; i < slideDist; i++)
                {
                    textPanel.Top -= 1;
                }
                textChange.Enabled = true;
                button1.Enabled = true;
                textChange.Clear();
            }


        }
        // When Key is up the User will stop
        private async void keyisup(object sender, KeyEventArgs e)
        {
            int millisecondsDelay = 2000;

            // User Movement
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            // When Button is Clicked, wait for some time and make text box not visible
            if (buttonClicked == true)
            {
                await Task.Delay(millisecondsDelay);
                textBox.Visible = false;
                buttonClicked = false;
            }

        }

        private void mainTimerEvent(object sender, EventArgs e)
        {
            // Set the Label to follow the User
            textBox.Left = (user.Left + 50);
            textBox.Top = (user.Top - 20);

            // User Movement and Bounds
            movement(host);
            if (client != null)
            {
                movement(client);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // If button is click change to true
            buttonClicked = true;

            // Change the text label and makes it visible
            textBox.Text = textChange.Text;
            textBox.Visible = true;

            // Disable the text box and button
            textChange.Enabled = false;
            button1.Enabled = false;

            // Moves the panel down
            for (int i = 0; i < slideDist; i++)
            {
                textPanel.Top += 1;
            }
        }

        private void textBox_Click(object sender, EventArgs e)
        {

        }

        private PictureBox CreateUser(string username, Color color)
        {
       
            PictureBox userPictureBox = new PictureBox();
            userPictureBox.Size = new Size(50, 50);
            userPictureBox.Location = new Point(200, 150);
            userPictureBox.BackColor = color;
            userPictureBox.Visible = true;
            userPictureBox.Name = username;

            return userPictureBox;

        }

        private void movement(PictureBox users)
        {

            if (goLeft == true && users.Left > 0)
            {
                users.Left -= speed;
            }
            if (goRight == true && users.Right < (panel.Width))
            {
                users.Left += speed;
            }
            if (goUp == true && users.Top > 0)
            {
                users.Top -= speed;
            }
            if (goDown == true && users.Top < (panel.Height - 50))
            {
                users.Top += speed;
            }
        }
    }
}
