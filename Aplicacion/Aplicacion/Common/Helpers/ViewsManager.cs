using Aplicacion.Common.Exceptions;
using Aplicacion.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Common.Helpers
{
    internal class ViewsManager
    {
        private readonly static Dictionary<string, ViewsManagerArgs> _viewsDictionary = new Dictionary<string, ViewsManagerArgs>();

        internal static readonly ViewsManager Current = new ViewsManager();

        internal static void RegisterView<TView, TViewModel>()
        {
            Add(typeof(TView), typeof(TViewModel));
        }

        public ViewsManagerArgs this[string key] => Get(key);

        internal static Page CreateView<TView>() => CreateView(typeof(TView));
        internal static Page CreateView<TView>(Dictionary<string, object> args) => CreateView(typeof(TView), args);
        
        internal static Page CreateView(Type view) => CreateView(view?.Name);
        internal static Page CreateView(Type view, Dictionary<string, object> args) => CreateView(view?.Name, args);

        internal static Page CreateView(string view) => GetPage(view);
        internal static Page CreateView(string view, Dictionary<string, object> args) => GetPage(view, args);

        private static Page GetPage(string view, Dictionary<string, object> args = null)
        {
            ViewsManagerArgs viewsManagerArgs = Current[view];
            Page page = Activator.CreateInstance(viewsManagerArgs.View) as Page;
            ViewModelBase viewModel = Activator.CreateInstance(viewsManagerArgs.ViewModel) as ViewModelBase;

            viewModel.Args = args ?? new Dictionary<string, object>();
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

            if(!dictionaryKey.EndsWith("Page") || dictionaryKey.EndsWith("View"))
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
