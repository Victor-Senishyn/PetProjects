using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handbook
{
    public static class IdGenerator
    {
        private static long _lastAssignedId = 0;
        public static bool IsDeserialized = false;
        public static long GenerateId()
        {
            if (!IsDeserialized)
                return _lastAssignedId++;
            throw new InvalidOperationException("Cannot generate ID.");
        }
        public static void LoadLastAssignedIndex()
        {
            try
            {
                if (File.Exists(Constants.PathToLastAssignedId))
                {
                    string content = File.ReadAllText(Constants.PathToLastAssignedId);
                    _lastAssignedId = long.Parse(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading last assigned ID: " + ex.Message);
            }
        }
        public static void SaveLastAssignedId()
        {
            try
            {
                File.WriteAllText(Constants.PathToLastAssignedId, _lastAssignedId.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving last assigned ID: " + ex.Message);
            }
        }
    }
}
