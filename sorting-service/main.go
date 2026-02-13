package main

import (
	"context"
	"fmt"
	"net"
	"sort"

	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	"sorting-service/sortingpb"
)

type sorterServer struct {
	sortingpb.UnimplementedSorterServer
}

func (s *sorterServer) SortPersons(ctx context.Context, req *sortingpb.SortPersonsRequest) (*sortingpb.SortPersonsResponse, error) {
	persons := append([]*sortingpb.Person(nil), req.GetPersons()...)
	sort.Slice(persons, func(i, j int) bool {
		return persons[i].Age < persons[j].Age
	})

	return &sortingpb.SortPersonsResponse{Persons: persons}, nil
}

func main() {
	lis, err := net.Listen("tcp", ":50051")
	if err != nil {
		panic(err)
	}

	grpcServer := grpc.NewServer()
	sortingpb.RegisterSorterServer(grpcServer, &sorterServer{})
	reflection.Register(grpcServer)

	fmt.Println("Go gRPC sorter runs on :50051")
	if err := grpcServer.Serve(lis); err != nil {
		panic(err)
	}
}
