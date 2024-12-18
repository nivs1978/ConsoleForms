using ConsoleForms;
using System.Drawing;


var formMain = new ConsoleForm();
formMain.Location = new Point(10, 5);
formMain.Text = "Options";

var groupBox = new GroupBox
{
    Text = "Printer",
    Location = new Point(2, 2),
    Size = new Size(20, 10),
    BorderColor = ConsoleColor.White,
    BackgroundColor = ConsoleColor.Black
};

formMain.Controls.Add(groupBox);

Application.Run(formMain);
Console.CursorTop = Console.WindowHeight-2;

