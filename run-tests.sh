#!/usr/bin/env bash

echo ""
echo "Run tests for MsCoreOne..."
echo ""

for test in ./tests/*/
do
    echo "Testing $test"
    pushd "$test"
    dotnet test --logger:trx
    popd
done