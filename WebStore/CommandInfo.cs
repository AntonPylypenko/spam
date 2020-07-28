namespace WebStore
{
    public delegate void Command();
    public class CommandInfo
    {
        public string name;
        public Command command;

        public CommandInfo(string name, Command command)
        {
            this.name = name;
            this.command = command;
        }
    };
}
