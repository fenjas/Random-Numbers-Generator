using System.Text;

namespace Random_Numbers_Generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> allTickets = [];
            int plays = 1;
            int replaced = 0;
            string tickets_file = "tickets.txt";
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.Clear();

            if (File.Exists(tickets_file)) File.Delete(tickets_file);

            void WriteTo(string msg)
            {
                Console.WriteLine(msg);
                using StreamWriter sw = File.AppendText(tickets_file);
                {
                    sw.WriteLine(msg);
                }
            }

            WriteTo("SUPER 5 TICKETS");
            WriteTo("---------------");
            
            if (args.Length != 0)
            {
                if (int.TryParse(args[0], out plays))
                {
                    if (plays <= 0) plays = 1;
                }
                else
                {
                    plays = 1;
                }
            }

            // Create tickets. Discard identical ones and re-play.
            for (int i = 0; i < plays; i++)
            {
                var numbers = Super5.Play(Guid.NewGuid().GetHashCode());
                var currentTicket = string.Join('-', numbers.Select(n => n.ToString("D2")));

                if (!allTickets.Contains(currentTicket))
                {
                    allTickets.Add(currentTicket);
                }
                else
                {
                    i--; // replay
                    replaced++;
                }
            }

            // Display / Save tickets
            allTickets.Sort();
            StringBuilder sb = new();
            foreach (var ticket in allTickets)
            {
                sb.AppendLine(ticket);
            }
            WriteTo(sb.ToString().TrimEnd());
            

            double o5 = Math.Round(Super5.CalculateOdds(5), 2);
            double o4 = Math.Round(Super5.CalculateOdds(4), 2);
            double o3 = Math.Round(Super5.CalculateOdds(3), 2);
            string p5 = Math.Round((1 / o5), 8).ToString("0.########");
            string p4 = Math.Round((1 / o4), 8).ToString("0.########");
            string p3 = Math.Round((1 / o3), 8).ToString("0.########");

            WriteTo("---------------");
            WriteTo("");
            WriteTo($"Odds of guessing 5 numbers per ticket is {o5} or p={p5}");
            WriteTo($"Odds of guessing 4 numbers per ticket is {o4} or p={p4}");
            WriteTo($"Odds of guessing 3 numbers per ticket is {o3} or p={p3}");
            WriteTo("");
            WriteTo("😀 GOOD LUCK 😀");
            WriteTo("");
            
            WriteTo($"DEBUG::Generated {plays} tickets");
            WriteTo($"DEBUG::Replaced {replaced} identical tickets");
        }
    }
}
