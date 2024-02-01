using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    public class Parsing
    {
        public string ParseUsername(string logindata)
        {
            string[] lines = logindata.Split('\n');
            return lines[0];
        }
        public string ParsePassword(string logindata)
        {
            try
            {
                string[] lines = logindata.Split('\n');
                return lines[1];
            } catch
            {
                return "error";
            }
        }
        public string ParseVersion(string logindata)
        {
            try
            {
                string[] lines = logindata.Split('\n');
                string version = lines[2].Split('|')[0];
                return version;
            } catch
            {
                return "error";
            }
            
        }
        public bool IsLoginPacket(string logindata)
        {
            var splitted = logindata.Split('\n');
            try
            {
                if (splitted.Length >= 3)
                {

                    if (logindata.Split('\n')[2].Split('|').Length >= 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            } catch(Exception ex) 
            { 
                return false;
            }
        }
    }
}
