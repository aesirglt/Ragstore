import { Flex, Spinner } from '@chakra-ui/react';

export function LoadingList() {
  return (
    <Flex justify="center" align="center" h="200px">
      <Spinner />
    </Flex>
  );
} 