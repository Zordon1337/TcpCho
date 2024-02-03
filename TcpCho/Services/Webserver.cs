using LibHTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpCho
{
    public class Webserver
    {
        /*
         * No longer used after 03-02-2024 update
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public static void Init()
        {
            HTTP http = new HTTP();
            PacketsUtil packet = new PacketsUtil();

            if(IsAdministrator())
            {
                new Thread(() => http.ListenMA(new string[] { "http://127.0.0.1:4200/", "http://localhost:4200/", "http://localhost:80/", "http://127.0.0.1:80/" })).Start();
            } else
            {
                new Thread(() => http.ListenMA(new string[] { "http://127.0.0.1:4200/", "http://localhost:4200/" })).Start();
            }
            new Thread(() => http.ListenMA(new string[] { "http://127.0.0.1:4200/", "http://localhost:4200/" })).Start();
            http.get("/panel", "text/html", (queryparams)=>
            {
                string ids = "";
                foreach(var user in Bancho.users)
                {
                    ids += $"{user.UserStats.userId}, ";
                }
                return $@"
<pre>
<form action='/ep' method='post'>
<h1>Empty Packet</h1>
<p>PacketID</p>
<input name='PacketID' id='PacketID'/>
<p>id</p>
<input name='Username' id='Username'/>
<input type='submit'/>
</form>
<br/>
<br/>
<form action='/epte' method='post'>
<h1>Empty Packet to everyone</h1>
<p>PacketID</p>
<input name='PacketID' id='PacketID'/>
<input type='submit'/>
</form>
<br/>
<br/>
<form action='/pte' method='post'>
<h1>Packet With string</h1>
<p>PacketID</p>
<input name='PacketID' id='PacketID'/>
<p>string</p>
<input name='data' id='data'/>
<p>id</p>
<input name='Username' id='Username'/>
<input type='submit'/>
</form>
<h1>Currently active ids: {ids}</h2>


</pre>";
            });
            http.post("/pte", "text/html", (postparams) =>
            {
                string packetid = "";
                int pid = 0;
                string data = "";
                string id = "";
                postparams.TryGetValue("PacketID", out packetid);
                postparams.TryGetValue("data", out data);
                postparams.TryGetValue("Username", out id);
                foreach(var user in Bancho.users)
                {
                    if(user.UserStats.userId == int.Parse(id))
                    {
                        //var stream = user.Stream;
                        MemoryStream ms = new MemoryStream();
                        Writer bw = new Writer(ms);
                        bw.Write(data);
                        bw.Flush();
                        pid = int.Parse(packetid);
                        packet.Write(user.Client, (RequestType)pid, false, ms);
                    }
                }

                return "";
            });
            http.post("/epte", "text/html", (postparams) =>
            {
                string packetid = "";
                int pid = 0;
                postparams.TryGetValue("PacketID", out packetid);
                foreach (var user in Bancho.users)
                {
                    pid = int.Parse(packetid);
                    packet.WriteEmptyPacket(user.Client, (RequestType)pid);
                }

                return "";
            });
            http.post("/ep", "text/html", (postparams) =>
            {
                string packetid = "";
                int pid = 0;
                string id = "";
                postparams.TryGetValue("PacketID", out packetid);
                postparams.TryGetValue("Username", out id);
                foreach (var user in Bancho.users)
                {
                    if (user.UserStats.userId == int.Parse(id))
                    {
                        pid = int.Parse(packetid);
                        packet.WriteEmptyPacket(user.Client,(RequestType)pid);
                    }
                }

                return "";
            });
            
        }*/
    }
}
