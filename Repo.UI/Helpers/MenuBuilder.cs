using Repo.Helpers.Extensions;
using System;
using System.Collections.Generic;
using Repo.Helpers.Validation;

namespace Repo.UI.Helpers
{
    public class MenuBuilder<T> where T : struct, IConvertible
    {
        private readonly ConsoleColor[] _colors =
        {
            ConsoleColor.White,
            ConsoleColor.Blue,
            ConsoleColor.Yellow,
            ConsoleColor.Green
        };

        public MenuBuilder(string title, uint nesting, IDictionary<T, Action> operations)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Title = title;
            Nesting = nesting;
            Operations = operations;
        }

        #region Properties

        public string Title { get; }
        public uint Nesting { get; }
        public IDictionary<T, Action> Operations { get; }

        #endregion

        #region Methods

        public void Build()
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = GetColor();

                    PrintMenu(Title, Nesting);

                    var key = Console.ReadLine();
                    T operation;
                    if (Enum.TryParse<T>(key, out operation))
                    {
                        if (Operations.ContainsKey(operation))
                        {
                            Operations[operation]();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Operation is not supported!");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (ValidationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void PrintMenu(string title, uint nesting)
        {
            Console.Title = title;
            var tabs = new String('\t', (int)nesting);
           
            foreach (var operationType in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{tabs}{(int)operationType}. {((Enum)operationType).ToDisplayValue()}");
            }

            Console.WriteLine($"{tabs}Press any key to exit");
        }

        private ConsoleColor GetColor()
        {
            return _colors[Nesting % _colors.Length];
        }

        #endregion
    }
}
