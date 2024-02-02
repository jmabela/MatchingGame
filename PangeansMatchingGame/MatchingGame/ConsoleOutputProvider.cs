using System;
using System.Diagnostics;
using System.Text;
using MatchingGame.Interfaces;

namespace MatchingGame
{

    /// <summary>
    /// Ths ConsoleOutputProvider class, provides outputs to the Console
    /// </summary>
    class ConsoleOutputProvider : IOutputProvider
    {
        public ConsoleOutputProvider() : this(Encoding.UTF8)
        {

        }

        public ConsoleOutputProvider(Encoding encoding)
        {
            Console.OutputEncoding = encoding;
        }
        /// <summary>
        /// Write the specified output to the console
        /// </summary>
        /// <param name="output">The output</param>
        public void Write(string output)
        {
            Console.Write(output);
        }

        /// <summary>
        /// Write the output with a new line
        /// </summary>
        /// <param name="output"></param>
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        /// <summary>
        /// Write an empty new line
        /// </summary>
        public void WriteLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Clear the output
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Log the message in the Debug window
        /// </summary>
        /// <param name="message"></param>
        void IOutputProvider.Log(string message)
        {
            Debug.WriteLine(message);
        }

        /// <summary>
        /// Set the color of the output
        /// </summary>
        /// <param name="color"></param>
        void IOutputProvider.SetColor(int color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
        }
    }
}
