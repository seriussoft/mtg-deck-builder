using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public class DefinitionAttribute : Attribute
  {
    public char Name { get; set; }

    public DefinitionAttribute(char name = '\0')
    {
      this.Name = name;
    }
  }
}
