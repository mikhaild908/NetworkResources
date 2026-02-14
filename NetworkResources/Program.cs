using System.Net;
//using System.Threading.Tasks.Dataflow;
using System.Net.NetworkInformation;

// See https://aka.ms/new-console-template for more information
Console.Write("Enter a valid web address: ");

var url = Console.ReadLine();

if (string.IsNullOrEmpty(url))
{
    Console.WriteLine("No URL provided. Exiting.");
    return;
}

var uri = new Uri(url);

Console.WriteLine($"URL: {url}");
Console.WriteLine($"Scheme: {uri.Scheme}");
Console.WriteLine($"Port: {uri.Port}");
Console.WriteLine($"Host: {uri.Host}");
Console.WriteLine($"Path: {uri.AbsolutePath}");
Console.WriteLine($"Query: {uri.Query}");

IPHostEntry entry = Dns.GetHostEntry(uri.Host);

Console.WriteLine($"Host Name: {entry.HostName} has the following IP addresses:");

foreach (var ip in entry.AddressList)
{
    Console.WriteLine($"  {ip}");
}

Console.WriteLine("");

try
{
    var ping = new Ping();
    Console.WriteLine($"Pinging {uri.Host}...");
    
    var reply = ping.Send(uri.Host);
    Console.WriteLine($"Ping to {uri.Host} - Status: {reply.Status}");

    if (reply.Status == IPStatus.Success)
    {
        Console.WriteLine($"Reply from {reply.Address}: Time={reply.RoundtripTime:N0}ms");
        // if you get a status TimedOut - probably means that the site is blocking ping requests, but it doesn't necessarily mean the site is down
        // to avoid DDoS attacks, many sites block ping requests, so a timeout doesn't necessarily indicate the site is down
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error pinging {uri.Host}: {ex.Message}");
}
