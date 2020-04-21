using System;
using System.ComponentModel;

namespace ElectronicJournal_Desktop.Infrastructure
{

    /* 
     * Чтобы объект мог полноценно реализовать механизм привязки, 
     * надо реализовать в его классе интерфейс INotifyPropertyChanged.
     * ViewModelBase - класса, который реализует данные интерфейс
     * и является базовым для всех ViewModel
     */
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
        }
        //Возникает при изменении значения свойства.
        public event PropertyChangedEventHandler PropertyChanged;
        /*
         * Когда объект класса изменяет значение свойства,
         * то он через событие PropertyChanged извещает систему об изменении свойства. 
         * А система обновляет все привязанные объекты.
         */
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}
