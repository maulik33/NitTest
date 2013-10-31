using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebControls
{
    public class MessageItem
    {
        private readonly List<string> _Items;

        public MessageItem()
        {
            _Items = new List<string>();
        }

        public int Count
        {
            get { return _Items.Count; }
        }

        public string this[int Index]
        {
            get { return _Items[Index]; }
        }

        public void Add(string Msg)
        {
            string s = "<li>" + Msg + "</li>";
            _Items.Add(s);
        }

        public void Clear()
        {
            _Items.Clear();
        }
    }
}