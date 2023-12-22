// Copyright(c) Lester J. Clark and Contributors.
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
        if (NetCommon.HasItems(docExample.Paras))
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

    // Creates and returns the DataExceptions from the DocExceptions object.
    /// <include path='items/GetDataExceptions/*' file='Doc/DataCommon.xml'/>
    public static DataExceptions GetDataExceptions(DocExceptions docExceptions)
    {
      DataExceptions retValue = null;

      if (NetCommon.HasItems(docExceptions))
      {
        retValue = new DataExceptions();
        foreach (DocException docException in docExceptions)
        {
          DataException dataException = new DataException()
          {
            CRef = docException.CRef,
            Text = docException.Text
          };
          retValue.Add(dataException);
        }
      }
      return retValue;
    }

    // Creates and returns the DataLinks from the DocLinks object.
    /// <include path='items/GetDataLinks/*' file='Doc/DataCommon.xml'/>
    public static DataLinks GetDataLinks(DocLinks docLinks)
    {
      DataLinks retValue = null;

      if (NetCommon.HasItems(docLinks))
      {
        retValue = new DataLinks();
        foreach (DocLink docLink in docLinks)
        {
          DataLink dataLink = new DataLink()
          {
            FileName = docLink.FileName,
            Text = docLink.Text
          };
          retValue.Add(dataLink);
        }
      }
      return retValue;
    }

    // Creates and returns the DataParams from the DocParams object.
    /// <include path='items/GetDataParams/*' file='Doc/DataCommon.xml'/>
    public static DataParams GetDataParams(DocParams docParams)
    {
      DataParams retValue = null;

      if (NetCommon.HasItems(docParams))
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
        || NetCommon.HasItems(docRemarks.Paras)))
      {
        retValue = new DataRemark()
        {
          Paras = new DataParas()
        };
        if (NetString.HasValue(docRemarks.Text))
        {
          retValue.Text = docRemarks.Text;
        }
        if (NetCommon.HasItems(docRemarks.Paras))
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

    // Creates and returns the DataParams from the DocParams object.
    /// <include path='items/GetDataTypeParams/*' file='Doc/DataCommon.xml'/>
    public static DataTypeParams GetDataTypeParams(DocTypeParams docTypeParams)
    {
      DataTypeParams retValue = null;

      if (NetCommon.HasItems(docTypeParams))
      {
        retValue = new DataTypeParams();
        foreach (DocTypeParam docParam in docTypeParams)
        {
          DataTypeParam dataTypeParam = new DataTypeParam()
          {
            Name = docParam.Name,
            Text = docParam.Text
          };
          retValue.Add(dataTypeParam);
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
        && NetCommon.HasItems(remark.Paras))
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
