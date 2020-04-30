using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.JiraApi
{
    public abstract class CollectionBuilder<T, TModel> where T : CollectionBuilder<T, TModel>
    {
        protected List<TModel> Models = new List<TModel>();
        public List<TModel> Build() => Models.ToList();
        public T Add(TModel model)
        {
            Models.Add(model);
            return this as T;
        }
        protected T For(int count, Action action)
            => For(count, (i, x) => action());
        protected T For(int count, Action<int> action)
            => For(count, (i, x) => action(i));
        protected T For(int count, Action<int, T> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i, this as T);
            }
            return this as T;
        }
    }
}
