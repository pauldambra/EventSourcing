#! /bin/bash

set -eu

docker pull eventstore/eventstore

docker stop eventstore || true && docker rm eventstore || true

docker run --name eventstore \
  -it \
  -p 2113:2113 \
  -p 1113:1113 \
   -e START_STANDARD_PROJECTIONS=True \
  eventstore/eventstore:latest