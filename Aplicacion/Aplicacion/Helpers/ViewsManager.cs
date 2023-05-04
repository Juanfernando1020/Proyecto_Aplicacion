using Aplicacion.Common.MVVM;
using Aplicacion.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Helpers
{
    internal class ViewsManager
    {
        private static Dictionary<string, ViewsManagerArgs> _viewsDictionary = new Dictionary<string, ViewsManagerArgs>();

        internal static ViewsManager Current = new ViewsManager();

        internal static void RegisterView<TView, TViewModel>()
        {
            Add(typeof(TView), typeof(TViewModel));
        }

        public ViewsManagerArgs this[string key] => Get(key);

        internal static Page CreateView<TView>()
        {
            ViewsManagerArgs viewsManagerArgs = Current[typeof(TView).Name];
            Page page = Activator.CreateInstance(viewsManagerArgs.View) as Page;
            ViewModelBase viewModel = Activator.CreateInstance(viewsManagerArgs.ViewModel) as ViewModelBase;

            page.BindingContext = viewModel;

            return page;
        }

        private ViewsManagerArgs Get(string dictionaryKey)
        {
            if(!_viewsDictionary.ContainsKey(dictionaryKey))
                throw new KeyNotFoundException(dictionaryKey);

            return _viewsDictionary[dictionaryKey];
        }

        private static void Add(Type view, Type viewModel)
        {
            string dictionaryKey = view.Name;

            if(_viewsDictionary.ContainsKey(dictionaryKey))
                throw new KeyWasAlreadyAddedException(dictionaryKey);

            if(!dictionaryKey.EndsWith("Page"))
                throw new ViewNameFormatException(dictionaryKey);

            if(!viewModel.Namespace.EndsWith("ViewModel"))
                throw new ViewModelNamespaceFormatException(viewModel.Name);

            _viewsDictionary.Add(dictionaryKey, new ViewsManagerArgs(view, viewModel));
        }
    }

    internal class ViewsManagerArgs
    {
        public Type View { get; set; }
        public Type ViewModel { get; set; }

        public ViewsManagerArgs(Type view, Type viewModel)
        {
            View = view;
            ViewModel = viewModel;
        }
    }
}
