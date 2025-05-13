using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chatbot_part02
{
    public class Program
    {
        static Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use a strong password with at least 12 characters and a mix of symbols.",
                "Avoid using personal information like your name or birthdate.",
                "Consider using a password manager to keep track of your credentials." } },
            { "scam", new List<string> {
                "Never click on suspicious links or download attachments from unknown sources.",
                "Scams often sound urgent—take time to verify before responding.",
                "If it sounds too good to be true, it probably is. Always double-check." } },
            { "privacy", new List<string> {
                "Adjust privacy settings on your social media accounts.",
                "Be mindful of the personal information you share online.",
                "Use private browsing modes when on public devices." } },
        };

        static string userName = "";

        delegate void BotResponse(string input);

        static void Main(string[] args)
        {
            new audioPlay();
            new Logo();
            PrintGreeting();
            ConversationLoop();
        }

        static void PrintGreeting()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Welcome to the Cybersecurity Awareness Bot");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Chatbot: ");
            TypewriterEffect("Hi, what is your name?");
            Console.WriteLine();

            Console.Write("User: ");
            userName = GetUserName();

            Thread.Sleep(500);
            Console.Write("Chatbot: ");
            TypewriterEffect($"Hi {userName}, I'm here to assist you with any cybersecurity questions like passwords, scams, or privacy. Ask me anything!");
            Console.WriteLine();
        }

        static string GetUserName()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 50 || !name.All(c => char.IsLetter(c) || c == ' ' || c == '_'))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Chatbot: Please enter a valid name (letters, spaces, underscores only, 3-50 characters).");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("User: ");
                }
                else
                    break;
            }
            return name;
        }

        static void TypewriterEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }

        static void ConversationLoop()
        {
            while (true)
            {
                Console.WriteLine("\n-------------------------------------------------");
                Console.WriteLine("What would you like to know about cybersecurity?");
                Console.WriteLine("Type 'exit' to end the conversation.");
                Console.WriteLine("-------------------------------------------------");
                Console.Write("User: ");

                string userInput = Console.ReadLine()?.Trim().ToLower();
                if (userInput == "exit")
                {
                    ExitConversation();
                    break;
                }
                ProcessUserInput(userInput);
            }
        }

        static void ProcessUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                DefaultResponse();
                return;
            }

            DetectSentiment(input);

            string[] words = input.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            var keywords = words.Select(w => w.ToLower()).ToList();

            foreach (var keyword in topicResponses.Keys)
            {
                if (keywords.Contains(keyword))
                {
                    GiveResponse(keyword);
                    return;
                }
            }

            DefaultResponse();
        }

        static void GiveResponse(string keyword)
        {
            List<string> responses = topicResponses[keyword];
            Random rand = new Random();
            int index = rand.Next(responses.Count);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Chatbot: ");
            Console.WriteLine(responses[index]);
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        static void DefaultResponse()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Chatbot: ");
            TypewriterEffect("I'm not sure I understand. Can you please rephrase that?");
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        static void ExitConversation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine(" Thank you for using the Cybersecurity Awareness Bot!");
            Console.WriteLine(" Be cautious, be aware online and protect your information");
            Console.WriteLine("--------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        static void DetectSentiment(string input)
        {
            Dictionary<string, string> sentiments = new Dictionary<string, string>()
            {
                {"worried", "It's okay to feel worried—cybersecurity can be intimidating, but I'm here to help!"},
                {"curious", "Curiosity is a great start to becoming cyber-aware! Ask away."},
                {"frustrated", "Sorry you're feeling frustrated. Let's try to simplify things."},
            };

            foreach (var sentiment in sentiments)
            {
                if (input.Contains(sentiment.Key))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Chatbot: ");
                    Console.WriteLine(sentiment.Value);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                }
            }
        }
    }
}
