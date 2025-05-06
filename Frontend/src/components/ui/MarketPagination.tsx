import { Pagination } from './Pagination';

interface MarketPaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export function MarketPagination({
  currentPage,
  totalPages,
  onPageChange,
}: MarketPaginationProps) {
  return (
    <Pagination
      currentPage={currentPage}
      totalPages={totalPages}
      onPageChange={onPageChange}
      variant="fixed"
    />
  );
} 