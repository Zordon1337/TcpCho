﻿using System;
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
            string[] lines = logindata.Split('\n');
            return lines[1];
        }
        public string ParseVersion(string logindata)
        {
            string[] lines = logindata.Split('\n');
            string version = lines[2].Split('|')[0];
            return version;
        }
        public bool IsLoginPacket(string logindata)
        {
            if(logindata.Split('\n').Length <= 4)
            {
                if (logindata.Split('\n')[2].Split('|').Length >= 2)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
    }
}