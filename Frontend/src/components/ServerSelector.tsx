'use client';

import { Select } from '@chakra-ui/react';
import { useServer } from '../contexts/ServerContext';
import { useServers, ServerResume } from '../hooks/useServers';
import { useEffect } from 'react';

export function ServerSelector() {
  const { currentServer, setCurrentServer } = useServer();
  const { data: serversData, isLoading } = useServers();

  useEffect(() => {
    if (serversData?.data && serversData.data.length > 0 && !currentServer) {
      setCurrentServer(serversData.data[0]);
    }
  }, [serversData, currentServer, setCurrentServer]);

  if (isLoading) {
    return <Select size="sm" width="200px" isDisabled placeholder="Carregando..." />;
  }

  return (
    <Select
      value={currentServer?.id || ''}
      onChange={(e) => {
        const selectedServer = serversData?.data.find((server: ServerResume) => server.id === e.target.value);
        if (selectedServer) {
          setCurrentServer(selectedServer);
        }
      }}
      size="sm"
      width="200px"
      placeholder="Selecione um servidor"
    >
      {serversData?.data.map((server: ServerResume) => (
        <option key={server.id} value={server.id}>
          {server.displayName}
        </option>
      ))}
    </Select>
  );
} 