E:
cd E:\tfs\DefaultCollection\Felinesoft.UmbracoCodeFirst\Dev Branches\twigs\refactor-converters
copy "DB restore\Umbraco - uninitialised.sdf" "Demo\App_Data"
del /q "Demo\App_Data\Umbraco.sdf"
ren "Demo\App_Data\Umbraco - uninitialised.sdf" "Umbraco.sdf"
pause