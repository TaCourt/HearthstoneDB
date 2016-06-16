using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1.Events
{
    public class ButtonPressedEvent
    {

        #region Singleton
        private static ButtonPressedEvent _myEvent
        {
            get; set;
        }

        private ButtonPressedEvent()
        {

        }

        public static ButtonPressedEvent GetEvent()
        {
            if (_myEvent == null)
            {
                _myEvent = new ButtonPressedEvent();
            }
            return _myEvent;

        }

        #endregion


        public event EventHandler Handler;

        public void OnButtonPressedHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }

    }
}
