namespace DiDemo.Cli
{
    internal class ConsoleLogger : Logging.ConsoleLogger
    {
        public override void Log(string message)
        {
            base.Log("    Log>>> " + message);
        }
    }
}
