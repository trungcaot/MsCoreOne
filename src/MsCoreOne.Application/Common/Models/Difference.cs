namespace MsCoreOne.Application.Common.Models
{
    public class Difference
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public Difference(string key, string value)
        {
            Key = key;

            Value = value;
        }
    }
}
