using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Attributes {
   public class EnsureMinimumElementsAttribute : ValidationAttribute {
      private readonly int _minElements;
      public EnsureMinimumElementsAttribute( int minElements ) {
         _minElements = minElements;
      }

      public override bool IsValid( object value ) {
         var list = value as ICollection;
         return list?.Count >= _minElements;
      }
   }
}
