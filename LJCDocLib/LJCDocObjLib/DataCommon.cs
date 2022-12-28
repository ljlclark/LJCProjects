// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataCommon.cs
using LJCDocXMLObjLib;
using LJCNetCommon;

namespace LJCDocObjLib
{
  // The static common data methods.
  /// <include path='items/DataCommon/*' file='Doc/DataCommon.xml'/>
  public class DataCommon
  {
    #region Methods

    // Creates and returns the DataExample object from the DocExample object.
    /// <include path='items/GetDataExample/*' file='Doc/DataCommon.xml'/>
    public static DataExample GetDataExample(DocExample docExample)
    {
      DataExample retValue = null;

      if (docExample != null)
      {
        retValue = new DataExample()
        {
          Paras = new DataParas(),
          Code = docExample.Code
        };
        if (docExample.Paras != null && docExample.Paras.Count > 0)
        {
          foreach (DocPara docPara in docExample.Paras)
          {
            DataPara dataPara = new DataPara()
            {
              Text = docPara.Text
            };
            retValue.Paras.Add(dataPara);
          }
        }
      }
      return retValue;
    }

    // Creates and returns the DataParams from the DocParams object.
    /// <include path='items/GetDataParams/*' file='Doc/DataCommon.xml'/>
    public static DataParams GetDataParams(DocParams docParams)
    {
      DataParams retValue = null;

      if (docParams != null && docParams.Count > 0)
      {
        retValue = new DataParams();
        foreach (DocParam docParam in docParams)
        {
          DataParam dataParam = new DataParam()
          {
            Name = docParam.Name,
            Text = docParam.Text
          };
          retValue.Add(dataParam);
        }
      }
      return retValue;
    }

    // Creates and returns the DataRemark object from the DocRemarks object.
    /// <include path='items/GetDataRemark/*' file='Doc/DataCommon.xml'/>
    public static DataRemark GetDataRemark(DocRemarks docRemarks)
    {
      DataRemark retValue = null;

      if (docRemarks != null
        && (NetString.HasValue(docRemarks.Text)
        || (docRemarks.Paras != null && docRemarks.Paras.Count > 0)))
      {
        retValue = new DataRemark()
        {
          Paras = new DataParas()
        };
        if (NetString.HasValue(docRemarks.Text))
        {
          retValue.Text = docRemarks.Text;
        }
        if (docRemarks.Paras != null && docRemarks.Paras.Count > 0)
        {
          foreach (DocPara docPara in docRemarks.Paras)
          {
            DataPara dataPara = new DataPara()
            {
              Text = docPara.Text
            };
            retValue.Paras.Add(dataPara);
          }
        }
      }
      return retValue;
    }

    // Retrieves the member name from the full name.
    /// <include path='items/GetMemberName/*' file='Doc/DataCommon.xml'/>
    public static string GetMemberName(string fullName, string prefix = null)
    {
      string retValue = null;

      if (prefix != null)
      {
        retValue = fullName.Substring(prefix.Length + 1);
        int index = retValue.IndexOf('(');
        if (index > -1)
        {
          // Strip arguments.
          retValue = retValue.Substring(0, index);
        }
      }
      else
      {
        int index = fullName.IndexOf('(');
        if (index > -1)
        {
          string text = fullName.Substring(0, index);
          index = text.LastIndexOf('.');
          retValue = text.Substring(index + 1);
        }
        else
        {
          index = fullName.LastIndexOf('.');
          if (index > -1)
          {
            retValue = fullName.Substring(index + 1);
          }
        }
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves the namespace from the full object name.
    /// </summary>
    /// <param name="fullName">The full object name.</param>
    /// <returns>The object namespace.</returns>
    public static string GetNamespace(string fullName)
    {
      string retValue = null;

      int index = fullName.IndexOf('(');
      if (index > -1)
      {
        string text = fullName.Substring(0, index);
        index = text.LastIndexOf('.');
        retValue = text.Substring(2, index - 2);
      }
      else
      {
        index = fullName.LastIndexOf('.');
        if (index > -1)
        {
          retValue = fullName.Substring(2, index - 2);
        }
      }
      return retValue;
    }

    // Retrieves and returns the Syntax value.
    /// <include path='items/GetSyntax/*' file='Doc/DataCommon.xml'/>
    public static string GetSyntax(DataRemark remark, out bool hasSyntax)
    {
      string retValue = null;

      hasSyntax = false;

      if (remark != null
        // && (LJCNetString.HasValue(remark.Text)
        && remark.Paras != null && remark.Paras.Count > 0)
      {
        if (remark.Paras != null && remark.Paras.Count > 0)
        {
          foreach (DataPara dataPara in remark.Paras)
          {
            if (dataPara.Text != null && dataPara.Text.Trim().StartsWith("Syntax:"))
            {
              hasSyntax = true;
              retValue = dataPara.Text.Trim().Substring("Syntax:".Length);
            }
          }
        }
      }
      return retValue;
    }

    // Checks if the syntax is public.
    /// <include path='items/IsPublic/*' file='Doc/DataCommon.xml'/>
    public static bool IsPublic(DataRemark remark, out bool hasSyntax)
    {
      bool retValue = false;

      string text = GetSyntax(remark, out hasSyntax);
      if (text != null)
      {
        if (text.Trim().StartsWith("public"))
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion
  }
}
