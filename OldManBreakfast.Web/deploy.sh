#!/bin/bash
dotnet publish -c Production
ssh -t woodstock "service oldmanbreakfast stop"
scp -rp bin/Production/netcoreapp2.0/publish/* woodstock:/home/oldmanbreakfast/public_html
ssh -t woodstock "chown -R oldmanbreakfast:oldmanbreakfast /home/oldmanbreakfast/public_html/*"
ssh -t woodstock "service oldmanbreakfast start"
