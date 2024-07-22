namespace Family_Tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person personOne = new Person("Граф", "Грэй", Gender.Men, 40);
            Person personSix = new Person("Графиня", "Грэй", Gender.Women, 40);
            Person personTwo = new Person("Сын", "Грэй", Gender.Men, 20);
            Person personThree = new Person("Дочь", "Грэй", Gender.Women, 15);
            Person personFour = new Person("Старший Граф", "Грэй", Gender.Men, 80);
            Person personFive = new Person("Старшая графиня", "Грэй", Gender.Women, 78);
            Person personSeven = new Person("Старший Маркиз", "Пауэр", Gender.Men, 76);
            Person personEight = new Person("Старшая Таркиза", "Пауэр", Gender.Women, 70);
            personOne.addKid(personTwo);
            personOne.addKid(personThree);
            personOne.addParents(personFive, personFour);
            personSix.addKid(personTwo);
            personSix.addKid(personThree);
            personSix.addParents(personSeven, personEight);
            personTwo.addParents(personOne, personSix);
            personThree.addParents(personOne, personSix);
            personFour.addKid(personOne);
            personFive.addKid(personOne);
            personSeven.addKid(personSix);
            personEight.addKid(personSix);
            personOne.addPartner(personSix);
            personSix.addPartner(personOne);

            //Console.WriteLine(personOne.GetKids());
            //Console.WriteLine(personOne.Mother?.Name);
            //Console.WriteLine(personTwo.GetGrand());
            Console.WriteLine(personOne.GetRelatives());
        }
    }
}
