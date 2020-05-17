using System.Collections.ObjectModel;
using WhiteMvvm.Bases;


namespace WhiteMvvm.Utilities
{
    public class TransitionalList<TBaseTransition> : Collection<TBaseTransition> where TBaseTransition : BaseTransitional
    {
        public virtual ObservableRangeCollection<TBaseModel> ToModel<TBaseModel>() where TBaseModel : BaseModel , new()
        {
            var newList = new ObservableRangeCollection<TBaseModel>();
            foreach (var transition in this)
            {                
                var BaseModel = transition.ToModel<TBaseModel>();
                newList.Add(BaseModel);
            }
            return newList;
        }
    }
}
