using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEFW
{
    public class FoodDAO
    {
        public List<Food> GetAll()
        {
            List<Food> foods;
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                foods =  ResEntities.Foods.ToList();
            }
            return foods;
        }
        public Food GetById(int id)
        {            
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                return ResEntities.Foods.FirstOrDefault(fo => fo.ID == id);                
            }            
        }

        public List<Food> GetByName(string name)
        {
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                return ResEntities.Foods.Where(fo => fo.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }
        }

        public List<Food> GetByAboveCalory(int CalNumber)
        {
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                return ResEntities.Foods.Where(fo => fo.Calories > CalNumber).ToList();
            }
        }
        public List<Food> SearchFoodsByCriteria(string name, int maxCal, int minCal,  int minGrade)
        {
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                return ResEntities.Foods.Where(
                    fo => fo.Calories > minCal &&
                    (fo.Grade < int.MaxValue && fo.Grade > minGrade) &&
                    fo.Calories < maxCal && (fo.Name == "" || fo.Name.ToUpper().Contains(name.ToUpper()))).ToList();
            }
        }
        public void AddFood(Food f)
        {
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                ResEntities.Foods.Add(f);
                ResEntities.SaveChanges();
            }
        }
        public void UpdateFood(Food f)
        {
            Food food = new Food();
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                food = ResEntities.Foods.FirstOrDefault(fo => fo.ID == f.ID);
                food.Name = f.Name;
                food.Ingridients = f.Ingridients;
                food.Calories = f.Calories;
                food.Grade = f.Grade;
                ResEntities.SaveChanges();
            }
        }
        public void RemoveFood(int id)
        {
            using (FoodDBEntities ResEntities = new FoodDBEntities())
            {
                ResEntities.Foods.Remove(ResEntities.Foods.FirstOrDefault(fo => fo.ID == id));
                ResEntities.SaveChanges();
            }
        }
    }
}
