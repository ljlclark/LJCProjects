using LJCGenTextXML5;
using LJCNetCommon5;

namespace TestGenText5
{
  internal class Program
  {
    static void Main()
    {
      var sectionManager = new SectionManager("DataXML.xml");
      if (LJC.HasListItems(sectionManager.Sections))
      {
        _ = new TestSectionsXML();
        _ = new TestItemsXML();
        _ = new TestReplacementsXML();

        Sections sections = sectionManager.Sections;

        var section = sectionManager.Retrieve("Fields");
        if (section != null)
        {
          var itemManager = new RepeatItemManager(sections);
          var repeatItem = new RepeatItem()
          {
            Name = "Item1",
          };
          itemManager.Add(section.Name, repeatItem);

          repeatItem = itemManager.Retrieve(section.Name, "Item1");
        }
      }
    }
  }
}
