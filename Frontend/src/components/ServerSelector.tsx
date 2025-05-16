'use client';

import { Select } from '@chakra-ui/react';
import { useServer } from '../contexts/ServerContext';

const servers = [
  { value: 'latamro', label: 'Latam RO' },
];

export function ServerSelector() {
  const { currentServer, setCurrentServer } = useServer();

  return (
    <Select
      value={currentServer}
      onChange={(e) => setCurrentServer(e.target.value)}
      size="sm"
      width="200px"
    >
      {servers.map((server) => (
        <option key={server.value} value={server.value}>
          {server.label}
        </option>
      ))}
    </Select>
  );
} 