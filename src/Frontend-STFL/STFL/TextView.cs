// Smuxi - Smart MUltipleXed Irc
// 
// Copyright (c) 2011 Mirco Bauer
// 
// Full GPL License: <http://www.gnu.org/licenses/gpl.txt>
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA

using System;
using System.Collections.Generic;

namespace Stfl
{
    public class TextView : Widget
    {
        public string OffsetVariableName { get; set; }
        List<string> Lines { get; set; }

        public int Offset {
            get {
                return Int32.Parse(Form[OffsetVariableName]);
            }
            set {
                Form[OffsetVariableName] = value.ToString();
            }
        }

        public int OffsetStart {
            get {
                return 0;
            }
        }

        public int OffsetEnd {
            get {
                int heigth = Heigth;
                if (Lines.Count <= heigth) {
                    return 0;
                }
                return Lines.Count - heigth;
            }
        }

        public TextView(Form form, string widgetId) :
                   base(form, widgetId)
        {
            Lines = new List<string>();
            Form.EventReceived += OnEventReceived;
        }

        public void Append(string text)
        {
            Lines.Add(text);
            // TODO: implement line wrap and re-wrap when console size changes
            Form.Modify(WidgetName, "append", "{" +
                        "listitem text:" + StflApi.stfl_quote(text) + "}");
        }

        public void ScrollUp()
        {
            Scroll(-0.9);
        }

        public void ScrollDown()
        {
            Scroll(0.9);
        }

        protected void Scroll(double scrollFactor)
        {
            int currentOffset = Offset;
            int newOffset = (int) (currentOffset + (Heigth * scrollFactor));
            if (newOffset < 0) {
                newOffset = 0;
            } else if (newOffset > OffsetEnd) {
                newOffset = OffsetEnd;
            }
            Offset = newOffset;
        }

        public void ScrollToStart()
        {
            Offset = OffsetStart;
        }

        public void ScrollToEnd()
        {
            Offset = OffsetEnd;
        }

        List<string> ResizeLine(string line)
        {
            var resizedLine = new List<string>();
            return resizedLine;
        }

        void Resize()
        {
        }

        void OnEventReceived(object sender, EventReceivedEventArgs e)
        {
            if (e.Event == "RESIZE") {
                Resize();
            }
        }
    }
}
