using PizzaStoreApp.Exceptions;
using PizzaStoreApp.Interfaces;
using PizzaStoreApp.Models;

namespace PizzaStoreApp.Repositories
{
    
    public class PizzaImageRepository : IRepository<int, PizzaImages>
    {
        List<PizzaImages> images = new List<PizzaImages>()
        {
            new PizzaImages(){Id = 1, Images = new List<string>(){"/images/margherita1.jpg", "/images/margherita2.jpg", "/images/margherita3.jpg"}},
            new PizzaImages(){Id = 2, Images = new List<string>(){"/images/pepperoni1.jpg", "/images/pepperoni2.jpg", "/images/pepperoni3.jpg"}}
        };
        public PizzaImages Add(PizzaImages item)
        {
            if (images.Count == 0)
                throw new CannotAddWithNoImagesException();
            images.Add(item);
            return item;
        }

        public PizzaImages Delete(int key)
        {
            var myImages = Get(key);
            if (myImages == null)
                throw new NoSuchImageException();
            images.Remove(myImages);
            return myImages;
        }

        public PizzaImages Get(int key)
        {
            var image = images.FirstOrDefault(i => i.Id == key);
            if (image == null)
                throw new NoSuchImageException();
            return image;
        }

        public List<PizzaImages> GetAll()
        {
            if (images.Count == 0)
                throw new NoImagesException();
            return images;
        }

        public PizzaImages Update(PizzaImages pizza)
        {
            if(pizza.Images.Count == 0)
                throw new CannotUpdateWithNoImagesException();
            var myImage = Get(pizza.Id);
            if (myImage == null)
                throw new NoSuchImageException();
            myImage.Images = pizza.Images;
            return myImage;
        }
    }
}
