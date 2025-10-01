// using System.ComponentModel.DataAnnotations;
// using System.Runtime.CompilerServices;
// using System.Text;
// using Microsoft.EntityFrameworkCore;
// using SQLitePCL;
// using webbhelpuf.Data.Models;
// using webbhelpuf.Factories;
// using webbhelpuf.Services;
// using webbhelpuf.Shared;

// namespace webbhelpuf.Helpers;

// //no filtering, no validation, just list creation
// static public class SessionCartHelper
// {
//     public static string ToString(List<CartItem> input)
//     {
//         var sb = new StringBuilder();

//         foreach (CartItem ci in input)
//         {
//             sb.Append(ci.Id + ":" + ci.Amount + ";");
//         }

//         return sb.ToString();
//     }
//     public static List<CartItem> ToList(string sessionValue)
//     {
//         var output = new HashSet<CartItem>();

//         var shopItemDict = new Dictionary<Guid, int>();
//         string[] sessionValueSplitted = sessionValue.Split(';');
//         foreach (string sessionValueItem in sessionValueSplitted)
//         {
//             string[] sessionValueItemSplitted = sessionValueItem.Split(':');

//             Guid id = Guid.Empty;
//             int amount = -1;

//             bool idok = Guid.TryParse(sessionValueItemSplitted[0], out id);
//             bool amountok = int.TryParse(sessionValueItemSplitted[1], out amount);

//             if (idok && amountok)
//             {
//                 if (!shopItemDict.ContainsKey(id))
//                 {
//                     shopItemDict.Add(id, 0);
//                 }
//                 shopItemDict[id] += amount;
//             }
//         }

//         return output.ToList();
//     }
// }