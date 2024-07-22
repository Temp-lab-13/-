using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Tree
{ 
    public enum Gender
    {
        Men,
        Women
    };

    public class Person
    {
        public Person(string? name, string? surname, Gender gender, int? age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Gender = gender;
            Age = age;
            this.Childrens = new List<Person>();
        }


        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }
        public int? Age { get; set; }
        public Person? Mother { get; set; }
        public Person? Father { get; set; }
        public Person? Partner { get; set; }
        public List<Person> Childrens { get; set; }
        /*
        public Person[] GetGrandMothers()
        {
            return new Person[]{Mother.Mother, Father.Mother};
           }
        public Person[] GetGrandFathers() 
        { 
            return new Person[] {Mother.Father, Father.Father }; 
        }
        */

        public bool addParent(Person parent)
        {
            if (parent.Gender.Equals(Gender.Men))
            {
                Father = parent;
                return true;

            } else if (parent.Gender.Equals(Gender.Women))
            {
                Mother = parent;
                return true;
            }
            return false;
        }

        public bool addParents(Person parentM, Person parentF)
        {
            if (Mother == null && Father == null)
            {
                addParent(parentM);
                addParent(parentF);
                return true;
            }
            return false;
        }


        public bool addKid(Person kid)
        {
            if (Childrens == null || !Childrens.Contains(kid))
            {
                Childrens?.Add(kid); 
                return true;
            }
            return false;
        }

        public bool addPartner(Person partner)
        {
            if (Partner == null)
            {
                Partner = partner;
                return true;
            }
            return false;
        }

        public string GetKids()
        {
            StringBuilder Child = new StringBuilder();
            Child.Append("Дети: ");
            if (Childrens.Count > 0)
            {
                foreach (Person child in Childrens)
                {
                    Child.Append(child.Name);
                    Child.Append(" ");
                    Child.Append(child.Surname);
                    Child.Append(", "); 
                }
                Child.Remove(Child.ToString().LastIndexOf(','), 1);
                return Child.ToString();
            }
            Child.Append("Детей нет;");
            return Child.ToString();
        }

        public string GetPartner()
        {
            if (Partner != null)
            {
                if(Partner.Gender.Equals(Gender.Men))
                    return $"Супруг: {Partner.Name}";
                if (Partner.Gender.Equals(Gender.Women))
                    return $"Супруга: {Partner.Name}";
            }
            return "Супруга нет;";
        }

        public string GetGrand()
        {
           StringBuilder grang = new StringBuilder();
            if (Father != null)
            {
                grang.Append("Родители Отца: ");
                if (Father.Father != null)
                    grang.Append(Father.Father.Name);
                grang.Append(" ");
                if (Father.Mother != null)
                    grang.Append(Father.Mother.Name);
            }
            else 
            { 
                grang.Append("Отец не известен."); 
            }
            grang.Append("\n");
            if (Mother != null)
            {
                grang.Append("Родители Матери: ");
                if (Mother.Father != null)
                    grang.Append(Mother.Father.Name);
                grang.Append(" ");
                if (Mother.Mother != null)
                    grang.Append(Mother.Mother.Name);
            }
            else
            {
                grang.Append("Мать не известна.");
            }
            return grang.ToString();
        }

        public string GetRelatives()
        {
            StringBuilder listPerson = new StringBuilder();
            listPerson.AppendLine($"Родственники {Name}:");
            listPerson.AppendLine(GetPartner());
            listPerson.AppendLine(GetKids());
            listPerson.AppendLine(GetGrand());
            return listPerson.ToString();
        }
    }
}
