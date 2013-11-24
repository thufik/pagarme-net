#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.ObjectModel;

namespace PagarMe
{
    internal class FreezableCollection<T> : Collection<T>, IFreezable
    {
        private bool _freezed;

        public void Freeze()
        {
            _freezed = true;
        }

        protected override void ClearItems()
        {
            if (_freezed)
                throw new InvalidOperationException("Collection is read-only.");

            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            if (_freezed)
                throw new InvalidOperationException("Collection is read-only.");

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (_freezed)
                throw new InvalidOperationException("Collection is read-only.");

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            if (_freezed)
                throw new InvalidOperationException("Collection is read-only.");

            base.SetItem(index, item);
        }
    }
}