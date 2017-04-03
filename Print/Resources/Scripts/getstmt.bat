@echo off
:Batch file to download statements data for printing
cd "C:\AS400Report\Scripts"

: Delete previous files
: del /Q statements.csv

: Download overlay file
ftp -s:getstmt.txt

lcd "C:\AS400Report\Print Queue\CSV Files"

: Run the AS400Report tool
cd "C:\AS400Report\AS400Report\bin\Debug"
start AS400Report Statements

echo Processing complete.
bye