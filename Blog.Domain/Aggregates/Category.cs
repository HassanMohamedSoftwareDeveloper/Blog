using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Aggregates
{
    public class Category : AggregateRoot<CategoryId>, IAggregateRoot<CategoryId>
    {
        #region Fields :
        private CategoryName _categoryName;
        #endregion

        #region CTORS :
        public Category(CategoryId categoryId, CategoryName categoryName)
        {
            this.Id = categoryId;
            this._categoryName = categoryName;
        }
        #endregion

        #region Methods :
        public void Update(CategoryName categoryName) => this._categoryName = categoryName;
        #endregion
    }
}
