using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeamProject
{
    public static class People
    {
        private static string serializingFile
        {
            get
            {
                var assemblyPath = Path.GetDirectoryName(typeof(Program).GetTypeInfo().Assembly.Location);
#if DEBUG
                return Path.Combine(Directory.GetParent(assemblyPath).Parent.Parent.FullName, "DreamTeamProject/Properties/users.json");
#else
                return Path.Combine(assemblyPath, "Properties/users.json");
#endif
            }
        }

        private static int lastId;
        public static int NextId { get => lastId++; }
        
        public static List<User> Users;

        static People()
        {
            lastId = 0;
            Users = new List<User>();
        }

        public static bool Serialize()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Users);
                File.WriteAllText(serializingFile, json);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Deserialize()
        {
            try
            {
                if (!File.Exists(serializingFile))
                {
                    return false;
                }
                string json = File.ReadAllText(serializingFile);
                Users = JsonConvert.DeserializeObject<List<User>>(json);
                lastId = Users.Count + 1;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
