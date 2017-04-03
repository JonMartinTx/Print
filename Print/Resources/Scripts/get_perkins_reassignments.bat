:@echo off
:Batch file to download spooled files from the AS/400
cd "C:\AS400Report\Scripts"

: Download spooled files
ftp -i -s:get_perkins_reassignments.txt

: Run the AS400Report tool
cd "C:\AS400Report\AS400Report\bin\Debug"
start AS400Report PerkinsReassignment

echo Processing complete.

bye
