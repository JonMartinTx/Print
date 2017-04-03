:@echo off
:Batch file to download spooled files from the AS/400
cd "C:\AS400Report\Scripts"

: Backup previous files
del prtq\*.* > Nul

cd "C:\AS400Report\Print Queue\prtq"

: Download spooled files
ftp -i -s:prtq.dat

: Run the AS400Report tool
cd "C:\AS400Report\AS400Report\bin\Debug"
start AS400Report Reports

echo Processing complete.