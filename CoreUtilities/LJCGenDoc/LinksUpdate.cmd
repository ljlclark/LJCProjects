echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem LinksUpdate.cmd

set root=..\..\..\LJCProjectsDev

rem **********************
rem *** CoreAssemblies ***
rem ---------------
set src=\CoreAssemblies\LJCAppManager\LJCAppManager\LinkPages
set to=HTML\LJCAppManager
copy %root%%src%\AppManagerTables.html %to%
copy %root%%src%\AppManagerTables.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDataAccess\LJCDataAccess\Diagrams
set to=HTML\LJCDataAccess
copy %root%%src%\DataAccessLink.html %to%
copy %root%%src%\DataAccessLink.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDataAccessConfig\LJCDataAccessConfig\Diagrams
set to=HTML\LJCDataAccessConfig
copy %root%%src%\DataAccessConfig.html %to%
copy %root%%src%\DataAccessConfig.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDataDetail\LJCDataDetail\Diagrams
set to=HTML\LJCDataDetail
copy %root%%src%\LJCDataDetail.html %to%
copy %root%%src%\LJCDataDetail.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\Diagrams
set to=HTML\LJCDBClientLib
copy %root%%src%\Configuration.html %to%
copy %root%%src%\Configuration.jpg %to%
copy %root%%src%\DataMethods.html %to%
copy %root%%src%\DataMethods.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDBClientLib\LJCDBClientLib\DocPages
set to=HTML\LJCDBClientLib
copy %root%%src%\DataManagerClass.html %to%
copy %root%%src%\DataManagerCode.html %to%
set to=HTML\LJCDBClientLib\Methods
copy %root%%src%\DataManagerAdd.html %to%
copy %root%%src%\DataManagerDelete.html %to%
copy %root%%src%\DataManagerLoad.html %to%
copy %root%%src%\DataManagerLoadProcedure.html %to%
copy %root%%src%\DataManagerRetrieve.html %to%
copy %root%%src%\DataManagerUpdate.html %to%

rem ---------------
set src=\CoreAssemblies\LJCDBMessage\LJCDBMessage\Diagrams
set to=HTML\LJCDBMessage
copy %root%%src%\DbRequestColumn.html %to%
copy %root%%src%\DbRequestColumn.jpg %to%
copy %root%%src%\DbRequestJoin.html %to%
copy %root%%src%\DbRequestJoin.jpg %to%
copy %root%%src%\DbRequestResult.html %to%
copy %root%%src%\DbResult.jpg %to%

set src=\CoreAssemblies\LJCDBMessage\LJCDBMessage
copy %root%%src%\DbMessageGraph.jpg %to%

rem ---------------
set src=\CoreAssemblies\LJCDBDataAccess\LJCDBDataAccess\Diagrams
set to=HTML\LJCDBDataAccess
copy %root%%src%\DbAccessConfig.html %to%
copy %root%%src%\DbAccessConfig.jpg %to%
copy %root%%src%\DbAccessData.html %to%
copy %root%%src%\DbAccessData.jpg %to%

rem *********************
rem *** CoreUtilities ***
set src=\CoreUtilities\LJCGenDoc\LJCDocObjLib\Diagrams
set to=HTML\LJCDocObjLib
copy %root%%src%\DataRootToMethod.html %to%
copy %root%%src%\DataRootToMethod.jpg %to%
copy %root%%src%\DataPropertyToExample.html %to%
copy %root%%src%\DataPropertyToExample.jpg %to%

set src=\CoreUtilities\LJCGenDoc\LJCDocXMLObjLib\Diagrams
set to=HTML\LJCDocXMLObjLib
copy %root%%src%\DocXML.html %to%
copy %root%%src%\DocXML.jpg %to%

set src=\CoreUtilities\LJCGenDoc\LJCGenDocDAL\LinkPages
set to=HTML\LJCGenDocDAL
copy %root%%src%\DocAssemblyTables.html %to%
copy %root%%src%\DocAssemblyTables.jpg %to%
copy %root%%src%\DocClassTables.html %to%
copy %root%%src%\DocClassTables.jpg %to%
copy %root%%src%\DocMethodTables.html %to%
copy %root%%src%\DocMethodTables.jpg %to%

rem ---------------
set src=\CoreUtilities\LJCGenDoc\LJCGenDocLib\Diagrams
set to=HTML\LJCGenDocLib
copy %root%%src%\GenRootToMethod.html %to%
copy %root%%src%\GenRootToMethod.jpg %to%
copy %root%%src%\GenPropertyToField.html %to%
copy %root%%src%\GenPropertyToField.jpg %to%

set src=\CoreUtilities\LJCGenDoc\LJCGenDocLib\CreateXML
copy %root%%src%\DocLibDataGraph.jpg %to%

set src=\CoreUtilities\LJCGenDoc\LJCGenDocLib\CreateXML
copy %root%%src%\DocLibXMLGraph.jpg %to%

rem ---------------
set src=\CoreUtilities\LJCSQLUtilLib\LJCSQLUtilLib\LinkPages
set to=HTML\LJCSQLUtilLib
copy %root%%src%\MetaDataTables.html %to%
copy %root%%src%\MetaDataTables.jpg %to%

rem ---------------
set src=\CoreUtilities\LJCViewEditor\LJCViewEditor\LinkPages
set to=HTML\LJCViewEditor
copy %root%%src%\ViewDataTables.html %to%
copy %root%%src%\ViewDataTables.jpg %to%
copy %root%%src%\ViewGridTables.html %to%
copy %root%%src%\ViewGridTables.jpg %to%
copy %root%%src%\ViewJoin.html %to%
copy %root%%src%\ViewJoin.jpg %to%

rem ******************
rem *** SampleApps ***
rem ---------------
set src=\SampleApps\LJCDocAppManager\LJCDocAppManager\LinkPages
set to=HTML\LJCDocAppManager
copy %root%%src%\DocAppTables.html %to%
copy %root%%src%\DocAppTables.jpg %to%

rem ---------------
set src=\SampleApps\LJCFacilityManager\LJCFacilityManager\LinkPages
set to=HTML\LJCFacilityManager
copy %root%%src%\BusinessTables.html %to%
copy %root%%src%\BusinessTables.jpg %to%
copy %root%%src%\FacilityTables.html %to%
copy %root%%src%\FacilityTables.jpg %to%
copy %root%%src%\PersonTables.html %to%
copy %root%%src%\PersonTables.jpg %to%

rem ---------------
set src=\SampleApps\GenealogyManager\GenealogyManager\LinkPages
set to=HTML\GenealogyManager
copy %root%%src%\GenealogyTables.html %to%
copy %root%%src%\GenealogyTables.jpg %to%

rem ---------------
set src=\SampleApps\LJCRegionManager\LJCRegionManager\LinkPages
set to=HTML\LJCRegionManager
copy %root%%src%\RegionTables.html %to%
copy %root%%src%\RegionTables.jpg %to%

rem ---------------
set src=\SampleApps\LJCSales\LJCSalesManager\LinkPages
set to=HTML\LJCSalesManager
copy %root%%src%\OrderTables.html %to%
copy %root%%src%\OrderTables.jpg %to%
copy %root%%src%\ProductTables.html %to%
copy %root%%src%\ProductTables.jpg %to%

rem ---------------
set src=\SampleApps\LJCDataTransform\LJCTransformManager\LinkPages
set to=HTML\LJCTransformManager
copy %root%%src%\ProcessTables.html %to%
copy %root%%src%\ProcessTables.jpg %to%
copy %root%%src%\SourceLayoutTables.html %to%
copy %root%%src%\SourceLayoutTables.jpg %to%
copy %root%%src%\TaskSourceTables.html %to%
copy %root%%src%\TaskSourceTables.jpg %to%
copy %root%%src%\TransformTables.html %to%
copy %root%%src%\TransformTables.jpg %to%
