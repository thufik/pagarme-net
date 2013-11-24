using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    internal class FreezableCollection<T> : Collection<T>, IFreezable
    {
        private bool _freezed;

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

        public void Freeze()
        {
            _freezed = true;
        }
    }
}
