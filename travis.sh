#!/bin/bash

set -e
set -o pipefail
export PATH="/opt/mono/bin:$PATH"
export LD_LIBRARY_PATH="/opt/mono/bin:/opt/mono/lib:$LD_LIBRARY_PATH"

mozroots --import --sync
xbuild CI.proj
mono --runtime=v4.0.30319 ./.nuget/NuGet.exe install -Version 2.6.3 -NonInteractive NUnit.Runners
mono --runtime=v4.0.30319 ./NUnit.Runners.2.6.3/tools/nunit-console.exe ./tests/PagarMe.Tests/bin/Debug_Net40/PagarMe.Tests.dll -exclude NotWorkingOnMono
