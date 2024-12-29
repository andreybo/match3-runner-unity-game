// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("z2XqJdlW9ihbKmfksi3QeqdZ3qhKTZpAHypWl3mQKMgTS2/pTBatjHkXAjh0EOd6zQt0n9JZnWeTavRwCz39nOy6Xd6WF7RFRofF8GXz1nPYkLG3PshQdoRt5MiPrkf9kUlHYP2Rbi+jJrNHHcKvK8quAoIqMkppy3PqtwnZNS7TXs+IvlEUtpgcTxvQU11SYtBTWFDQU1NSw+VAr4WAIS6+YTJzdj5nr/HSmt4g3qeojZrEYtBTcGJfVFt41BrUpV9TU1NXUlGO2H/DxAfKeEJI7LBxvDhTyHd10dF6YdTbsjpZy3klvYX/yZJCr7oux8tPLzrP3iMYDFyx4stY+3DAz8Vmyyub79dWjv+pwCZrOf62TD0bUXSKJQtkxQGvu1BRU1JT");
        private static int[] order = new int[] { 11,6,3,3,8,13,12,12,12,11,13,11,13,13,14 };
        private static int key = 82;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
