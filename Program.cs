using labs_RPM.Bridge;
using labs_RPM.Core;
using labs_RPM.Decorator;
using labs_RPM.Flyweight;
using labs_RPM.Proxy;

namespace labs_RPM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Тест Bridge
            Console.WriteLine("--- Bridge Pattern ---");
            Shape circle = new Circle(new VectorRenderer());
            circle.Draw();

            // 2. Тест Decorator
            Console.WriteLine("\n--- Decorator Pattern ---");
            Shape decoratedCircle = new ShadowDecorator(new BorderDecorator(circle));
            decoratedCircle.Draw();

            // 3. Тест Proxy
            Console.WriteLine("\n--- Proxy Pattern ---");
            IImage image = new ProxyImage("high_res_photo.png");
            Console.WriteLine("Image object created. Not loaded yet.");
            image.Display(); // Загрузка произойдет только здесь

            // 4. Тест Flyweight
            Console.WriteLine("\n--- Flyweight Pattern ---");
            var factory = new CharacterFactory();
            var char1 = factory.GetCharacter('A', "Arial");
            var char2 = factory.GetCharacter('A', "Arial");
            char1.Display(12);
            char2.Display(14);
            Console.WriteLine($"Are same objects? {ReferenceEquals(char1, char2)}");
        }
    }
}