using System;

namespace WpfSample.Common
{
    public class SaveErrorMessageExcenption : Exception
    {
        public SaveErrorMessageExcenption(string message) : base(message)
        {
        }
    }
}
