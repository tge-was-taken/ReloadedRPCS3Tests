using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Reloaded.Mod.Interfaces;

namespace ReloadedRPCS3TestsNoConfig
{
    public class ConsoleLogger : ILogger, IDisposable
    {
        private readonly StreamWriter mWriter;

        public Color BackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color TextColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorRed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorRedLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorGreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorGreenLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorYellow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorYellowLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorBlue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorBlueLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorPink { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorPinkLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorLightBlue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ColorLightBlueLight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<string> OnPrintMessage;

        public ConsoleLogger()
        {
            mWriter = new StreamWriter( new FileStream( "log.txt", FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.WriteThrough ) );
        }

        public void PrintMessage( string message, Color color )
        {
            WriteLine( message, color );
        }

        public void Write( string message, Color color )
        {
            Write( message );
        }

        public void Write( string message )
        {
            Task.Run( () =>
            {
                lock ( mWriter )
                {
                    Console.Write( message );
                    mWriter.Write( message );
                }
            } );
        }

        public void WriteLine( string message, Color color )
        {
            WriteLine( message );
        }

        public void WriteLine( string message )
        {
            Task.Run( () =>
            {
                lock ( mWriter )
                {
                    Console.WriteLine( message );
                    mWriter.WriteLine( message );
                }
            } );
        }

        public void Dispose()
        {
            mWriter.Dispose();
        }
    }
}
