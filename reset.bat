E:
cd E:\tfs\Felinesoft.UmbracoCodeFirst
copy "DB restore\Umbraco - uninitialised.sdf" "Demo\App_Data"
del /q "Demo\App_Data\Umbraco.sdf"
ren "Demo\App_Data\Umbraco - uninitialised.sdf" "Umbraco.sdf"
pause